<template>
  <div class="demo-view">
    <h1>{{ label }}</h1>
    <Flex>
      <Button type="primary">按钮</Button>
    </Flex>
    <br />
    <Flex gap="middle" vertical>
      <RadioGroup v-model:value="value">
        <Radio value="horizontal">horizontal</Radio>
        <Radio value="vertical">vertical</Radio>
      </RadioGroup>
      <Flex :vertical="value === 'vertical'">
        <div
          v-for="(item, index) in new Array(4)"
          :key="item"
          :style="{ ...baseStyle, background: `${index % 2 ? '#1677ff' : '#1677ffbf'}` }"
        />
      </Flex>
    </Flex>
  </div>
</template>

<script setup lang="ts">
import { ref } from 'vue'
import { Button, Flex, RadioGroup, Radio } from 'ant-design-vue'
import type { CSSProperties } from 'vue'

const value = ref('horizontal')
const baseStyle: CSSProperties = {
  width: '30%',
  height: '54px',
}
const label = ref('测试页面')
</script>

<style lang="css" scoped></style>
