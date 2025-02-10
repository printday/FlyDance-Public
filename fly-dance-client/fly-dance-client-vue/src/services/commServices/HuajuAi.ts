import { type AxiosInstance } from 'axios'
import getAxios from '@/utils/Axios'

/**
 * 搜索框服务类
 */
class HuajuAiService {
  private axios: AxiosInstance

  /**
   * 初始化
   * @param axiosInstance 服务请求类对象
   */
  constructor(axiosInstance: AxiosInstance) {
    this.axios = axiosInstance
  }

  /**
   * 聊天接口
   * @param text 内容
   * @returns 聊天接口请求
   */
  chat(text: string) {
    return this.axios.get('/huaju-ai/Index/Chat?text=' + text)
  }
}

export function useHuajuAiService() {
  return new HuajuAiService(getAxios())
}
