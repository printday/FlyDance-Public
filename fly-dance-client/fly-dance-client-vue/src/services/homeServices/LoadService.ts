import { type AxiosInstance } from 'axios'
import getAxios from '@/utils/Axios'

/**
 * 内容加载服务类
 */
class LoadService {
  private axios: AxiosInstance

  /**
   * 初始化
   * @param axiosInstance 服务请求类对象
   */
  constructor(axiosInstance: AxiosInstance) {
    this.axios = axiosInstance
  }

  /**
   * 加载首页服务
   */
  loadService() {
    return this.axios.get('/FlySystemWeb/api/Service/GetHomeService')
  }
}

export function useLoadService() {
  return new LoadService(getAxios())
}
