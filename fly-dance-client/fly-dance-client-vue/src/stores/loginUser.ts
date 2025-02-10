import { ref, computed } from 'vue'
import { defineStore } from 'pinia'

export const useLoginUserStore = defineStore('loginUser', () => {
  const userId = ref('')
  const getUserId = computed(() => userId)
  function setUserId(id: string) {
    userId.value = id
  }

  return { userId, getUserId, setUserId }
})
