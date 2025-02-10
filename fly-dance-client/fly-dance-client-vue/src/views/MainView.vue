<template>
  <div class="main-veiw">
    <Flex class="main-box" vertical>
      <Flex class="head-box" vertical>
        <SearchComp />
        <!-- <LoginOut /> -->
      </Flex>
      <MenuOption class="content-box" />
    </Flex>
  </div>
</template>

<script lang="ts" setup>
import { Flex } from 'ant-design-vue'

import SearchComp from '@/components/commons/SearchComp.vue'
import MenuOption from '@/components/layouts/heads/MenuOption.vue'
</script>

<style lang="css" scoped>
.head-box {
  background-color: var(--fly-background-color);
  width: var(--app-width);
  height: calc(var(--app-height) / 20);
}
.content-box {
  width: 100%;
  height: calc(var(--app-height) / 1.1217);
  overflow: scroll;
}
</style>
