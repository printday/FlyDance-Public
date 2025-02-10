import axios from 'axios'
import type { App } from 'vue'

const axiosInstance = axios.create({
  baseURL: import.meta.env.VITE_APP_API_URL || 'https://8.138.162.59',
  timeout: 10000,
})

// 请求拦截器
axiosInstance.interceptors.request.use(
  (config) => {
    return config
  },
  (error) => {
    return Promise.reject(error)
  },
)

// 响应拦截器
axiosInstance.interceptors.response.use(
  (response) => {
    return response
  },
  (error) => {
    return Promise.reject(error)
  },
)

export default function install(app: App) {
  app.provide('axios', axiosInstance)
}
