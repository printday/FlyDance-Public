<template>
  <!-- 搜索框 -->
  <div class="search-comp">
    <Input type="text" placeholder="搜索无限乐趣,还有pizza🍕.." v-model:value="searchStr">
      <template #prefix>
        <!-- <span style="font-weight: bold">月半 猫</span> -->
        <SecurityScanOutlined class="searcg-icon" />
      </template>
      <template #suffix>
        <Button @click="Search">🍭</Button>
      </template>
    </Input>
  </div>
</template>

<script lang="ts" setup>
import { Input, Button, message } from 'ant-design-vue'
import { SecurityScanOutlined } from '@ant-design/icons-vue'
import { ref } from 'vue'
import { useRouter } from 'vue-router'
import { useSearchService } from '@/services/commServices/SearchService'

const searchService = useSearchService()
const router = useRouter()
const searchStr = ref('')

const Search = async () => {
  searchService
    .getUser(searchStr.value)
    .then((response) => {
      console.log('尝试请求获取的返回：', response.data)
      message.info(
        searchStr.value.trim() == '' ? '什么都没写还想搜索？奖励月半猫一只' : response.data.userId,
      )
      return response.data
    })
    .catch((err) => {
      return `请求错误${err}`
    })
}
</script>

<style lang="css" scoped>
.searcg-icon {
  font-size: calc(var(--app-width) / 18);
}
</style>
