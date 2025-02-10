<template>
  <Flex class="home-view" vertical>
    <Skeleton :paragraph="skeletonState" :loading="showLogin" active>
      <Card :key="item.id" v-for="item in cards" class="home-card-item">
        {{ item.name }}
      </Card>
    </Skeleton>
  </Flex>
</template>

<script setup lang="ts">
import { Flex, Card, Skeleton, type SkeletonProps, message } from 'ant-design-vue'
import { ref } from 'vue'

import type { Service } from '@/models/homes/Service'
import { useLoadService } from '@/services/homeServices/LoadService'

const skeletonState = ref({ rows: 19 } as SkeletonProps)
const showLogin = ref(true)
const cards = ref([] as Service[])

const loadService = useLoadService()

loadService
  .loadService()
  .then((resp) => {
    if (resp.data.isok) {
      showLogin.value = false
      cards.value = resp.data.data
    } else {
      message.error('首页内容加载失败')
    }
  })
  .catch((err) => {
    console.error(err)
    message.error('获取数据失败，请检查网络是否连接')
  })
</script>

<style lang="css" scoped>
.home-view {
  width: 100%;
  background: var(--fly-home-backgroud-color);
  padding: var(--home-view-padding-updown) var(--home-view-padding-about);
}

.home-card-item {
  margin-top: 5px;
}
</style>
