<template>
  <div class="text-input">
    <Input v-model:value="chatText" type="text">
      <template #suffix>
        <Button @click="send" :disabled="sending">Send</Button>
      </template>
    </Input>
  </div>
</template>

<script lang="ts" setup>
import { Button, Input, message } from 'ant-design-vue'
import { useHuajuAiService } from '@/services/commServices/HuajuAi'
import { ref } from 'vue'

const aiService = useHuajuAiService()
const chatText = ref('')

const sending = ref(false)

const send = () => {
  sending.value = true
  if (chatText.value == undefined || chatText.value == null || chatText.value.trim() == '') {
    message.info('请输入内容！')
    return
  }
  aiService
    .chat(chatText.value)
    .then((response) => {
      sending.value = false
      if (response.data == null || !response.data.isok || !response.data.data) {
        message.info('请求失败')
        return
      }
      if (response.data.data.choices == null || response.data.data.choices.lenght <= 0) {
        message.info('ai无语')
        return
      }
      const respMessage = response.data.data.choices[0]
      message.success(respMessage.message.content)
    })
    .catch((err) => {
      sending.value = false
      message.error('系统错误')
      console.error(err)
    })
}
</script>

<style lang="css" scoped>
.text-input {
  width: 100%;
  position: fixed;
  bottom: 0;
}
</style>
