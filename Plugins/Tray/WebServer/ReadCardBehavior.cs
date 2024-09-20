using Newtonsoft.Json;
using NLog;
using System;
using WebSocketSharp;
using WebSocketSharp.Server;
using YiJian.CardReader.WebServer;

namespace YiJian.CardReader.CardReaders
{
    public class ReadCardBehavior : WebSocketBehavior
    {
        private static readonly ILogger _log = LogManager.GetCurrentClassLogger();
        private ICardReader? _reader;

        public void SetupCardReader(ICardReader reader)
        {
            _reader = reader;
        }

        protected override void OnClose(CloseEventArgs e)
        {
            base.OnClose(e);
            _reader?.CloseDevice();
        }

        protected override void OnError(ErrorEventArgs e)
        {
            base.OnError(e);
        }

        protected override void OnMessage(MessageEventArgs e)
        {
            if (e.Data.Equals("IsHere**"))//客户端定时发送心跳，维持链接
                return;
            if (string.IsNullOrWhiteSpace(e.Data)) return;

            var tscode = JsonConvert.DeserializeObject<InputInfo>(e.Data);
            if (tscode == null) return;

            try
            {
                _log.Info("{CurrentTime} 收到 {Data}", DateTime.Now, e.Data.ToString());
                switch (tscode.tsCode)
                {
                    case "ReadIDCard":
                        {
                            var idCardInfo = _reader?.ReadIdCard();
                            Send(JsonConvert.SerializeObject(JsonResult.Ok(data: idCardInfo)));
                            break;
                        }
                    case "ReadSicard":
                        {
                            //JsonResult<MedicareCardInfo> resultMed = GetMedicareCardInfo();
                            var cardInfo = _reader?.ReadMedicareCard();
                            Send(JsonConvert.SerializeObject(JsonResult.Ok(data: cardInfo)));
                            break;
                        }
                    case "ReadM1":
                        {
                            //JsonResult<RFIDCardInfo> resultRFID = GetRFIDCardInfo();
                            var cardInfo = _reader?.ReadRfCard();
                            Send(JsonConvert.SerializeObject(JsonResult.Ok(data: cardInfo)));
                            break;
                        }
                    case "ReadMG":
                        {
                            _reader?.ReadMagneticCard();
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                _log.Error("读卡失败：{Error}", ex.ToString());
                Send(JsonConvert.SerializeObject(JsonResult.Fail($"读卡失败：{ex.Message}")));
            }

        }

        protected override void OnOpen()
        {
            base.OnOpen();
            try
            {
                if (false == _reader?.OpenDevice())
                {
                    Send(JsonConvert.SerializeObject(JsonResult.Fail($"读卡器连接失败")));
                }
            }
            catch(Exception ex)
            {
                Send(JsonConvert.SerializeObject(JsonResult.Fail($"读卡器连接失败：{ex.Message}")));
            }
        }
    }
}
