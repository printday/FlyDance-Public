import { type AxiosInstance } from 'axios'
import getAxios from '@/utils/Axios'
import type { LoginForm, RegisterForm } from '@/models/loginModels/LoginForm.d.ts'

/**
 * 搜索框服务类
 */
class LoginService {
  private axios: AxiosInstance

  /**
   * 初始化
   * @param axiosInstance 服务请求类对象
   */
  constructor(axiosInstance: AxiosInstance) {
    this.axios = axiosInstance
  }

  /**
   * 登录
   */
  login(loginReq: LoginForm) {
    return this.axios.post('/FlySystemWeb/api/Login/Login', loginReq)
  }

  /**
   * 注册
   */
  register(registerReq: RegisterForm) {
    return this.axios.post('/FlySystemWeb/api/Login/Register', registerReq)
  }

  /**
   * 发送验证码
   */
  sendCode(email: string) {
    return this.axios.post('/FlySystemWeb/api/Login/SendCode', { email: email })
  }
}

export function useLoginService() {
  return new LoginService(getAxios())
}
