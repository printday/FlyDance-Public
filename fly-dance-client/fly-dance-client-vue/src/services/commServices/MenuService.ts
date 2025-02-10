import { type AxiosInstance } from 'axios'
import getAxios from '@/utils/Axios'
import { useLoginUserStore } from '@/stores/loginUser'

/**
 * 搜索框服务类
 */
class MenuService {
  private axios: AxiosInstance

  /**
   * 初始化
   * @param axiosInstance 服务请求类对象
   */
  constructor(axiosInstance: AxiosInstance) {
    this.axios = axiosInstance
  }

  getContentMenu() {
    const loginUser = useLoginUserStore()
    const req = { userId: loginUser.getUserId.value }
    return this.axios.post('/FlySystemWeb/api/Menu/GetContentMenu', req)
  }
}

export function useMenuService() {
  return new MenuService(getAxios())
}
