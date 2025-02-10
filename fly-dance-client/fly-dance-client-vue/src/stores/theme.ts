import { ref, computed } from 'vue'
import { defineStore } from 'pinia'

export const useThemeStore = defineStore('theme', () => {
  const isLight = ref(true)
  const nowTheme = computed(() => (isLight.value ? 'light' : 'dark'))
  function setTheme(matches: boolean) {
    console.log('主题切换为：', matches ? '亮色系' : '暗色系')
    isLight.value = matches
  }

  return { isLight, nowTheme, setTheme }
})
