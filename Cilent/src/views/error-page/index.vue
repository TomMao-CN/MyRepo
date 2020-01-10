<template>
  <div class="error-page">
    <div class="error-img">
      <img
        class="face-image"
        src="@/assets/404_face.png"
        width="250px"
        alt="404 not found"
      />
    </div>
    <div class="error-msg">
      <h2>
        <strong>页面不存在</strong>
      </h2>
      <p>抱歉！我们找不到您想访问的页面...</p>
      <br />
      <a class="btn btn-default" href="https://leetcode-cn.com">
        <i class="fa fa-home" aria-hidden="true"></i> &nbsp;返回主页
      </a>
    </div>
  </div>
</template>
<style lang="scss">
.error-page {
  position: absolute;
  width: 40%;
  left: 50%;
  top: 50%;
  margin-left: -20%;
  margin-top: -15%;
  padding: 20px;
  .error-img {
    // position: absolute;
    float: left;
  }
  .error-msg {
    // position: absolute;
    float: left;
    margin-left: 20px;
  }
}
</style>