<template>
  <Watermark content="Fly Dance">
    <Flex class="regster-view" justify="center" align="center" vertical
      ><div class="login-title">Fly Dance</div>
      <RegisterForm />
    </Flex>
  </Watermark>
</template>

<script lang="ts" setup>
import { Watermark, Flex } from 'ant-design-vue'

import RegisterForm from '@/components/logins/RegisterForm.vue'
</script>

<style lang="css" scoped>
.regster-view {
  background-color: var(--fly-background-color);
  width: var(--app-width);
  height: var(--app-height);
}
.login-title {
  color: var(--fly-text-color);
  font-size: 30px;
}
</style>
