import { inject } from 'vue'
import { type AxiosInstance } from 'axios'

interface InjectedAxios {
  axios: AxiosInstance
}

/**
 * 获取配置的Axios实例
 * @returns axios
 */
const getAxios = () => {
  const axios = inject<InjectedAxios['axios']>('axios')
  if (!axios) {
    throw new Error('Axios未提供!')
  }
  return axios
}

export default getAxios
