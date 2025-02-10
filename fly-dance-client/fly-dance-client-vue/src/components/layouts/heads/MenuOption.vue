<template>
  <Tabs :tabPosition="mode" class="content-menu">
    <TabPane
      v-for="item in menus"
      :key="item.roid"
      :tab="item.routeName"
      class="content-menu-item"
      @change="changeTab(item.pathUrl)"
    >
      <RouterView class="router-view" />
    </TabPane>
    <template #leftExtra>
      <div style="width: 10px"></div>
    </template>
    <template #rightExtra>
      <div style="width: 10px"></div>
    </template>
  </Tabs>
</template>

<script lang="ts" setup>
import { onMounted, ref } from 'vue'
import { Tabs, TabPane, type TabsProps, message } from 'ant-design-vue'
import { useRouter } from 'vue-router'

import { useMenuService } from '@/services/commServices/MenuService'
import type { MenuInfo } from '@/models/commons/MenuInfo'

const router = useRouter()
const menuService = useMenuService()
const mode = ref<TabsProps['tabPosition']>('top')

const menus = ref([] as MenuInfo[])

const changeTab = (url: string) => {
  router.push(url)
}

onMounted(() => {
  menuService
    .getContentMenu()
    .then((resp) => {
      if (resp.data.isok && resp.data.data != null) {
        menus.value = resp.data.data
      } else {
        message.info(resp.data.message)
      }
    })
    .catch((err) => {
      console.error('获取服务错误：', err)
      message.error('获取服务失败，请检查网络是否连接！')
    })
})
</script>

<style lang="css" scoped>
.content-menu {
  background-color: var(--fly-background-color);
  width: var(--app-width);
  height: calc(var(--app-height) / 9.7);
  padding: 0;
  margin: 0px;
}

.content-menu-item {
  padding: 0;
  margin: 0;
}

.router-view {
  width: 100%;
  height: calc(var(--app-height) / 1.2);
  overflow: scroll;
}
</style>
