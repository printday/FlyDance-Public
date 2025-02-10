/**
 * 登录用表单类型
 * @author huaju
 */
export interface LoginForm {
  userid: string
  email: string
  username: string
  password: string
  loginType: number
}

/**
 * 注册用表单类型
 * @author huaju
 */
export interface RegisterForm {
  username: string
  email: string
  password: string
  passwordConfirm: string
  key: string
  code: string
}
