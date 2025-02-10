// import './assets/main.css' //  移除全局样式，采用Antd的全局样式
// 引入自定义全局css
import './assets/fly-dance-base.css'

import Antd from 'ant-design-vue'
import { createApp } from 'vue'
import { createPinia } from 'pinia'

import 'ant-design-vue/dist/reset.css'
import App from './App.vue'
import router from './router'

import axiosInstance from '@/plugins/baseAxios'

const app = createApp(App)

console.log(
  '%cFlyDance',
  'background-color: white; color: blue; border: 1px solid red; padding: 5px;font-size: 20px;',
)

app.use(axiosInstance)

app.use(createPinia())
app.use(router)
app.use(Antd)
app.mount('#app')
