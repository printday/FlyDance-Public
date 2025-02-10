<template>
  <Watermark content="Fly Dance">
    <Flex class="login-view" justify="center" align="center" vertical>
      <div class="login-title">Fly Dance</div>
      <LoginForm class="login-form" /> </Flex
  ></Watermark>
</template>

<script lang="ts" setup>
import { Watermark, Flex } from 'ant-design-vue'
import LoginForm from '@/components/logins/LoginForm.vue'
</script>

<style lang="css" scoped>
.login-view {
  background-color: var(--fly-background-color);
  width: 100%;
  height: var(--app-height);
}

.login-form {
  width: 70%;
}

.login-title {
  color: var(--fly-text-color);
  font-size: 30px;
}
</style>
