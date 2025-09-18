import { createRouter, createWebHistory } from 'vue-router'
import DefaultView from '../views/DefaultView.vue'
import BlacksmithView from '@/views/BlacksmithView.vue'
import NeedBoostView from  '@/views/NeedBoostView.vue'
import TargetView from '@/views/TargetView.vue'
import EditTargetView from '@/views/EditTargetsView.vue'
import UsersView from '@/views/UsersView.vue'
import LoginView from '@/views/LoginView.vue'


const routes = [
  {
    path: '/',
    name: 'default',
    component: DefaultView
  },
  {
    path: '/blacksmith',
    name: 'about',
    component: BlacksmithView
  },
  {
    path: '/needsomeboost',
    name: 'needboost',
    component: NeedBoostView
  },
  {
    path: '/target',
    name: 'target',
    component: TargetView
  },
  {
    path: '/edittarget',
    name: 'edittarget',
    component: EditTargetView
  },
  { path: '/users', // added by Andrew to test...
    name: 'users', 
    component: UsersView 
  },
  {
    path: '/login',
    name: 'login',
    component: LoginView
  }

]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
})

export default router
