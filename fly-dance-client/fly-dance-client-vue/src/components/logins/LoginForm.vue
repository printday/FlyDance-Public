<template>
  <Form
    :model="formState"
    name="loginForm"
    layout="vertical"
    autocomplete="off"
    @finish="onFinish"
    @finishFailed="onFinishFailed"
  >
    <FormItem
      label="Username"
      name="username"
      :rules="[{ required: true, message: 'Hey!Press You UserName!' }]"
    >
      <Input v-model:value="formState.username" />
    </FormItem>

    <FormItem
      label="Password"
      name="password"
      :rules="[{ required: true, message: 'Hey!Press You Password!' }]"
    >
      <InputPassword v-model:value="formState.password" />
    </FormItem>

    <FormItem name="remember">
      <Checkbox v-model:checked="remember">Remember me</Checkbox>
    </FormItem>

    <FormItem name="GoRegiste" class="form-item-right">
      <FlyA href="register"> 去注册 </FlyA>
    </FormItem>

    <FormItem class="form-item-center">
      <Button type="primary" html-type="submit">Submit</Button>
    </FormItem></Form
  >
</template>

<script lang="ts" setup>
import { Form, FormItem, Button, Input, InputPassword, Checkbox, message } from 'ant-design-vue'
import { onMounted, ref } from 'vue'
import type { ValidateErrorEntity } from 'ant-design-vue/es/form/interface'
import { useRouter } from 'vue-router'

import type { LoginForm } from '@/models/loginModels/LoginForm.d.ts'
import { useLoginService } from '@/services/loginServices/LoginService'
import FlyA from '@/components/commons/FlyA.vue'
import { useLoginUserStore } from '@/stores/loginUser'
import { getCookie } from '@/utils/Cookie'

const router = useRouter()
const loginService = useLoginService()
const loginUserStore = useLoginUserStore()
const cookieUilt = getCookie()

const remember = ref(false)
const formState = ref<LoginForm>({
  userid: '',
  email: '',
  username: '',
  password: '',
  loginType: 1,
})

const onFinish = () => {
  loginService
    .login(formState.value)
    .then((resp) => {
      if (resp.data.isok && resp.data.data != null) {
        const userId = resp.data.data.userId
        if (remember.value) {
          cookieUilt.setCookie('LoginUserId', userId, 3)
        }
        loginUserStore.setUserId(userId)
        message.success(resp.data.message)
        router.push('home')
      } else {
        message.error('登录失败，请检查用户名或密码！')
      }
    })
    .catch((err) => {
      console.log('请求错误...', err)
      message.error('登录请求失败,请稍后再试...')
    })
}

const onFinishFailed = (errorInfo: ValidateErrorEntity) => {
  console.log('Failed:', errorInfo)
}

const checkCookieIsLogin = () => {
  const userId = cookieUilt.getCookie('LoginUserId')
  if (userId) {
    loginUserStore.setUserId(userId)
    router.push('home')
  }
}

onMounted(() => {
  cookieUilt.removeCookie('LoginUserId')
  checkCookieIsLogin()
})
</script>
