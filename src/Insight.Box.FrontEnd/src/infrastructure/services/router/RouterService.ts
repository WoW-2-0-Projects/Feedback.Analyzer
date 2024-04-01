/*
 * Provides route information and managing the route
 */
import type {RouteLocationNormalized, Router, RouteRecordRaw} from "vue-router";
import {useRouter} from "vue-router";
import {ParameterizedNotificationSource} from "@/infrastructure/models/delegates/ParameterizedNotificationSource";

export class RouterService {

    constructor() {
    }

    private readonly routeChangeSource = new ParameterizedNotificationSource<RouteLocationNormalized>;
    private router!: Router;

    /*
     * Adds route change listener
     */
    public addRouteChangeListener(callback: (route: RouteLocationNormalized) => void): void {
        this.ensureRouteSet();
        this.routeChangeSource.addListener(callback);
    }

    /*
     * Gets current route
     */
    public getCurrentRoute(): RouteLocationNormalized {
        this.ensureRouteSet();
        return this.router.currentRoute.value;
    }

    /*
     * Gets current route
     */
    public getRoutes(): Array<RouteRecordRaw> {
        return [
            {
                name: 'Home',
                path: '/',
                component: () => import('../../../common/views/HomeView.vue')
            },
            {
                name: 'Organizations',
                path: '/organizations',
                component: () => import('../../../modules/organizations/components/OrganizationsTab.vue')
            },
            {
                name: 'Products',
                path: '/products',
                component: () => import('../../../modules/products/components/ProductsTab.vue')
            },
             {
                 name: 'Prompts',
                 path: '/prompts',
                 component: () => import('../../../modules/prompts/components/PromptsTab.vue')
             },
            {
                name: 'Workflows',
                path: '/workflows',
                component: () => import('../../../modules/workflows/components/WorkflowsTab.vue')
            }
        ]
    }

    private ensureRouteSet() {
        if(!this.router) {
            this.router = useRouter();

            this.router.afterEach((to) => {
                this.routeChangeSource.updateListeners(to);
            });
        }
    }
}