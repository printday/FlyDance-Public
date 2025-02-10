<template>
  <Flex class="menu-foot">
    <div :key="item.key" v-for="item in menus"></div>
  </Flex>
</template>

<script lang="ts" setup>
import { ref } from 'vue'
import { Flex } from 'ant-design-vue'
import type { FlyMenuObj } from '@/models/foots/Menu'

const menus = ref<FlyMenuObj[]>([])
</script>

<style lang="css" scoped>
.menu-foot {
  width: 100%;
  color: var(--fly-text-color);
  background-color: var(--fly-background-color);
}
</style>
