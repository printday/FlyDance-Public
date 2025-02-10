<!-- FlyDance的超链接组件 -->
<template>
  <div class="fly-link" @click="goRoute">
    <slot></slot>
  </div>
</template>

<script lang="ts" setup>
import { useRouter } from 'vue-router'

/**
 * 自定义超链接组件属性(App内的路由)
 */
interface FlyAProps {
  href: string
}
const router = useRouter()
const props = defineProps<FlyAProps>()

const goRoute = () => {
  router.push(props.href)
}
</script>

<style lang="css" scoped>
.fly-link {
  color: #1890ff;
  text-decoration: none;
}

.fly-link:hover {
  text-decoration: underline;
}
</style>
