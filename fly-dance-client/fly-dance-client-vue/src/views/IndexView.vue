<template>
  <div class="index-view">
    <div class="head-show">
      <div class="head-title">
        <div class="head-label">FlyDance</div>
      </div>
    </div>
    <Button class="go-login-button" type="primary" @click="goMain">去登录</Button>
    <!-- <Watermark content="Fly Dance"> -->
    <Flex>
      <div class="dashboard">
        <div class="dashboard-item">
          <span class="dashboard-item-label">完成度：</span>
          <Progress :size="70" type="circle" :percent="1" :format="(content) => `${content}%`" />
        </div>
      </div>
    </Flex>
    <!-- </Watermark> -->
  </div>
</template>

<script setup lang="ts">
import { Flex, Progress, Button } from 'ant-design-vue'
import { useRouter } from 'vue-router'

const router = useRouter()

const goMain = () => {
  router.push('login')
}
</script>

<style scoped>
@import url('@/assets/index.css');
</style>
