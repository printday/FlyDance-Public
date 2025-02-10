<template>
  <div class="login-out">
    <Button @click="loginOut">退出登录</Button>
  </div>
</template>

<script lang="ts" setup>
import { Button } from 'ant-design-vue'
import { message } from 'ant-design-vue'

const loginOut = () => {
  message.info('退出登录!')
}
</script>
