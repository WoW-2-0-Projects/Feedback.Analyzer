import {createRouter, createWebHistory} from 'vue-router'
import HomeView from '../../common/views/HomeView.vue'
import {RouteName} from "@/infrastructure/router/RouteName";

const router = createRouter({
    history: createWebHistory(import.meta.env.BASE_URL),
    routes: [
        {
            path: '/',
            name: RouteName.Home,
            component: () => import('../../common/views/HomeView.vue')
        },
        {
            path: '/organizations',
            name: RouteName.Organizations,
            component: () => import('../../modules/organizations/components/OrganizationsTab.vue')
        },
        {
            path: '/about',
            name: RouteName.About,
            component: () => import('../../common/views/HomeView.vue')
        }
    ]
})

export default router
