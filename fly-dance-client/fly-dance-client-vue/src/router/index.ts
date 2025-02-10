import { createRouter, createWebHashHistory, createWebHistory } from 'vue-router'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  // history: createWebHashHistory(), // 允许地址栏
  routes: [
    {
      path: '/',
      name: 'index',
      component: () => import('@/views/IndexView.vue'),
    },
    {
      path: '/demo',
      name: '测试',
      component: () => import('@/views/DemoView.vue'),
    },
    {
      path: '/login',
      name: '登录页',
      component: () => import('@/views/LoginView.vue'),
    },
    {
      path: '/register',
      name: '注册页',
      component: () => import('@/views/RegisterView.vue'),
    },
    {
      path: '/huaju-ai',
      name: 'huaju-ai',
      component: () => import('@/views/ais/HuajuAi.vue'),
    },
    {
      path: '/content',
      name: '主内容',
      component: () => import('@/views/ContentView.vue'),
      children: [
        {
          path: '/main',
          name: '主页',
          component: () => import('@/views/MainView.vue'),
          children: [
            {
              path: '/home',
              name: '首页',
              component: () => import('@/views/homes/HomeView.vue'),
            },
          ],
        },
      ],
    },
  ],
})

export default router
