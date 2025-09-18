import { createApp } from 'vue'
import App from './App.vue'
import store from './components/LoggedInnUser'
import router from './router'

createApp(App).use(router).use(store).mount('#app')
