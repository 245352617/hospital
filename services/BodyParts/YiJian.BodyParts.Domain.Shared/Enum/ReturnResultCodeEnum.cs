namespace YiJian.BodyParts
{
    public enum ReturnResultCodeEnum
    {
        未知状态 = -1,
        初始状态 = 0,
        成功返回200 = 200,
        警告返回201 = 201,
        程序报错500 = 500,
        获取登录授权失败401 = 401,
        没有权限访问403 = 403,
        找不到页面404 = 404,
        请求方式出错405 = 405,
        请求MetaType出错415 = 415
    }
}