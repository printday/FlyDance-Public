<template>
  <Form
    class="register-form"
    :model="formState"
    :rules="rules"
    name="registerForm"
    layout="vertical"
    autocomplete="off"
    @finish="goRegister"
    @finishFailed="goRegisterFailed"
  >
    <FormItem label="UserName" name="username" :rules="rules.username">
      <Input v-model:value="formState.username" />
    </FormItem>
    <FormItem label="Email" name="email" :rules="rules.email">
      <Input v-model:value="formState.email" />
    </FormItem>

    <FormItem label="Password" name="password" :rules="rules.password">
      <InputPassword v-model:value="formState.password" />
    </FormItem>

    <FormItem label="PasswordConfirm" name="passwordConfirm" :rules="rules.password">
      <InputPassword v-model:value="formState.passwordConfirm" />
    </FormItem>

    <FormItem label="Code" name="code" :rules="rules.code">
      <InputSearch v-model:value="formState.code" placeholder="验证码" @search="sendCode">
        <template #enterButton>
          <Button :disabled="isSend">{{ codeTitle }}</Button>
        </template>
      </InputSearch>
    </FormItem>

    <FormItem class="form-item-center">
      <Button type="primary" html-type="submit">Submit</Button>
    </FormItem></Form
  >
</template>

<script lang="ts" setup>
import { Form, FormItem, Button, Input, InputPassword, InputSearch, message } from 'ant-design-vue'
import { ref, reactive } from 'vue'
import type { RuleObject, ValidateErrorEntity } from 'ant-design-vue/es/form/interface'
import { useRouter } from 'vue-router'

import type { RegisterForm } from '@/models/loginModels/LoginForm.d.ts'
import { useLoginService } from '@/services/loginServices/LoginService'

const router = useRouter()
const loginService = useLoginService()

// 定义表单规则
const rules = reactive({
  code: [{ required: true, message: 'Hey!Code!' }] as RuleObject[],
  username: [
    { required: true, message: 'Hey!Press You UserName!' },
    {
      pattern: /^.{4,}$/, // 密码正则
      message: 'Username must be greater than 4 digits',
      trigger: ['blur', 'change'],
      type: 'string',
    },
  ] as RuleObject[],
  password: [
    { required: true, message: 'Hey!Press You Password!' },
    {
      pattern: /^.{6,}$/, // 密码正则
      message: 'Password must be greater than 6 digits',
      trigger: ['blur', 'change'],
      type: 'string',
    },
  ] as RuleObject[],
  email: [
    {
      required: true,
      message: 'Hey! Please enter your Email!',
      trigger: 'blur',
      type: 'string',
    },
    {
      pattern: /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/, // 邮箱正则
      message: 'The input is not a valid email address',
      trigger: ['blur', 'change'],
      type: 'string',
    },
  ] as RuleObject[],
})

const isSend = ref(false)
const codeTitle = ref('发送验证码')

const formState = ref<RegisterForm>({
  username: '',
  email: '',
  password: '',
  passwordConfirm: '',
  key: '',
  code: '',
})

/**
 * 发送验证码
 */
const sendCode = () => {
  createTimer(60)
}

/**
 * 真实注册
 */
const goRegister = () => {
  if (!isSend.value) {
    message.error('请先发送验证码')
    return
  }
  if (formState.value.password != formState.value.passwordConfirm) {
    message.error('两次输入的密码不一致')
    return
  }
  loginService
    .register(formState.value)
    .then((resp) => {
      if (resp.data.isok && resp.data.data) {
        message.success(resp.data.message)
        router.push('login')
      } else {
        if (resp.data) {
          message.error(resp.data.message)
        } else {
          message.error('注册失败！')
        }
      }
    })
    .catch((err) => {
      console.log('注册接口出错：', err)
    })
  return
}

/**
 * 创建一个计时器(记录验证码间隔)
 */
const createTimer = (time: number) => {
  if (formState.value.email) {
    loginService
      .sendCode(formState.value.email)
      .then((resp) => {
        if (resp.data.isok && resp.data.data) {
          message.success(resp.data.message)
          formState.value.key = resp.data.data
          isSend.value = true
          let labelTime: number = time

          const codeTime = setInterval(() => {
            codeTitle.value = `重新发送(${labelTime})`
            if (labelTime > 0) {
              labelTime -= 1
            }
          }, 1000)

          setTimeout(
            () => {
              clearInterval(codeTime)
              codeTitle.value = '重新发送'
              isSend.value = false
            },
            (time + 2) * 1000,
          )
        }
      })
      .catch((err) => {
        message.info('验证码发送失败')
      })
    return
  }
  message.info('请填写邮箱！')
}

/**
 * 注册请求发送失败
 */
const goRegisterFailed = (errorInfo: ValidateErrorEntity) => {
  console.log('Failed:', errorInfo)
}
</script>

<style lang="css" scoped>
.register-form {
  width: 80%;
}
</style>
