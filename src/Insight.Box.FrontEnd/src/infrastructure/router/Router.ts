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
            path: `/${RouteName.Organizations}`,
            name: RouteName.Organizations,
            component: () => import('../../modules/organizations/components/OrganizationsTab.vue')
        },
        {
            path: `/${RouteName.Products}`,
            name: RouteName.Products,
            component: () => import('../../modules/products/components/ProductsTab.vue')
        },
        {
            path: `/${RouteName.Prompts}`,
            name: RouteName.Prompts,
            component: () => import('../../modules/prompts/components/PromptsTab.vue')
        },
        {
            path: `/${RouteName.Workflows}`,
            name: RouteName.Workflows,
            component: () => import('../../modules/analysisWorkflows/components/WorkflowsTab.vue')
        },
        {
            path: '/about',
            name: RouteName.About,
            component: () => import('../../common/views/HomeView.vue')
        }
    ]
})

export default router
