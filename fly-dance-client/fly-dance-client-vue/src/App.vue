<template>
  <ConfigProvider :theme="themeConfig">
    <RouterView class="main-view" />
  </ConfigProvider>
</template>

<script lang="ts" setup>
import { ref, onMounted } from 'vue'
import { ConfigProvider, theme } from 'ant-design-vue'
import type { ThemeConfig } from 'ant-design-vue/es/config-provider/context'
import { useThemeStore } from './stores/theme'

const themeStore = useThemeStore()

/**
 * 整理全局配置(暗色系)
 */
const themeConfigDark = {
  token: {
    colorPrimary: '#303030',
  },
  components: {
    Radio: {
      colorPrimary: '#303030',
    },
    Menu: {
      colorItemBgActive: '#ffffff',
    },
    Tabs: {
      colorPrimary: '#0074D9',
    },
  },
  algorithm: theme.darkAlgorithm, // 默认算法defaultAlgorithm、暗色算法darkAlgorithm、紧凑算法compactAlgorithm
} as ThemeConfig

/**
 * 整理全局配置(亮色系)
 */
const themeConfigLight = {
  token: {
    colorPrimary: '#0047AB',
  },
  components: {
    Radio: {
      colorPrimary: '#0047AB',
    },
  },
  algorithm: theme.defaultAlgorithm, // 默认算法defaultAlgorithm、暗色算法darkAlgorithm、紧凑算法compactAlgorithm
} as ThemeConfig

const themeConfig = ref<ThemeConfig>(themeConfigDark)

// 检测系统主题并设置初始主题
function setThemeBasedOnSystemPreference() {
  const prefersDarkScheme = window.matchMedia('(prefers-color-scheme: dark)')
  themeConfig.value = prefersDarkScheme.matches ? themeConfigDark : themeConfigLight
  themeStore.setTheme(!prefersDarkScheme.matches)
}

// 监听系统主题变化
function setupThemeChangeListener() {
  const prefersDarkScheme = window.matchMedia('(prefers-color-scheme: dark)')
  prefersDarkScheme.addEventListener('change', (event) => {
    themeConfig.value = event.matches ? themeConfigDark : themeConfigLight
    themeStore.setTheme(!event.matches)
  })
}

// 在组件挂载时初始化主题设置和监听器
onMounted(() => {
  setThemeBasedOnSystemPreference()
  setupThemeChangeListener()
})
</script>

<style scoped></style>
