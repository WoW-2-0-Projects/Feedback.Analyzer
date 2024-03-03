import {createRouter, createWebHistory} from 'vue-router'
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
            path: '/prompts',
            name: RouteName.Prompts,
            component: () => import('../../modules/prompts/components/PromptsTab.vue')
        },
        {
            path: '/organizations',
            name: RouteName.Organizations,
            component: () => import('../../modules/organizations/components/OrganizationsTab.vue')
        },
        {
            path: '/products',
            name: RouteName.Products,
            component: () => import('../../modules/products/components/ProductsTab.vue')
        },
        {
            path: '/about',
            name: RouteName.About,
            component: () => import('../../common/views/HomeView.vue')
        }
    ]
})

export default router
