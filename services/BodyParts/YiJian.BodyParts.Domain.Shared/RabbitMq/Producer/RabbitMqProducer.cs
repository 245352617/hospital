using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace YiJian.BodyParts.Domain.Shared.RabbitMq.Producer
{
    public class RabbitMqProducer : IRabbitMqProducer
    {
        private IConnection _connection = null;
        private IModel _channel = null;
        private readonly object _lock = new object();
        private readonly ConnectionFactory _connectionFactory;

        public RabbitMqProducer(ConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public void Publish<T>(string exchange,string routingKey,T t)
        {
            var strMsg = JsonConvert.SerializeObject(t);
            Publish(exchange, routingKey, strMsg);
        }

        public void Publish(string exchange, string routingKey,string msg)
        {
            setChannel();
            if(_channel == null)
            {
                Console.WriteLine("发送RabbitMq消息失败");
                return;
            }
            _channel.BasicPublish(exchange, routingKey, null, Encoding.UTF8.GetBytes(msg));
        }

        public void Publish(string queueName,string msg)
        {
            setChannel();
            if(_channel == null)
            {
                Console.WriteLine("发送RabbitMq消息失败");
                return;
            }
            _channel.BasicPublish("", queueName, null, Encoding.UTF8.GetBytes(msg));
        }
        

        private void setConnection()
        {
            if (_connection != null) return;

            lock (_lock)
            {
                if (_connection != null) return;

                try
                {
                    _connectionFactory.RequestedConnectionTimeout = TimeSpan.FromSeconds(20);
                    _connection = _connectionFactory.CreateConnection();
                }
                catch (Exception se)
                {
                    Console.WriteLine(se);
                }
            }
        }

        private void setChannel()
        {
            if (_channel != null && !_channel.IsClosed) return;

            setConnection();
            if (_connection == null) return;

            try
            {
                _channel = _connection.CreateModel();
            }
            catch (Exception se)
            {
                Console.WriteLine(se);
            }
        }
    }
}
