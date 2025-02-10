import { type AxiosInstance } from 'axios'
import getAxios from '@/utils/Axios'
import { useRouter, type Router } from 'vue-router'

/**
 * 搜索框服务类
 */
class SearchService {
  private axios: AxiosInstance
  private router: Router

  /**
   * 初始化
   * @param axiosInstance 服务请求类对象
   */
  constructor(axiosInstance: AxiosInstance) {
    this.axios = axiosInstance
    this.router = useRouter()
  }

  /**
   * 根据id获取用户
   * @param content 内容
   */
  getUser(content: string) {
    console.log(content)
    if (content == 'huajuAi') {
      this.router.push('huaju-ai')
    }
    return this.axios.post('/FlySystemWeb/api/User/GetUserById', {
      id: content,
    })
  }
}

export function useSearchService() {
  return new SearchService(getAxios())
}
