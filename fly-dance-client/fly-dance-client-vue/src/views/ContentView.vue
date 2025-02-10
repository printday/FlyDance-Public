<template>
  <Flex vertical>
    <RouterView class="main-box" />
    <Flex class="foot-box"> <MenuFoot /> </Flex>
  </Flex>
</template>
<script lang="ts" setup>
import { RouterView } from 'vue-router'
import { Flex } from 'ant-design-vue'
import MenuFoot from '@/components/layouts/foots/MenuFoot.vue'
</script>

<style lang="css" scoped>
.main-box {
  width: 100%;
  height: calc(var(--app-height) / 1.062);
}
.foot-box {
  background-color: var(--fly-background-color);
  width: 100%;
  height: calc(var(--app-height) / 17);
}
</style>
