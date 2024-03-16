/*
 * Provides route information and managing the route
 */
import type {RouteLocationNormalized, Router, RouteRecordRaw} from "vue-router";
import {useRouter} from "vue-router";
import {NotificationSource} from "@/infrastructure/models/notifications/Action";

export class RouterService {

    constructor() {
    }

    private readonly routeChangeSource = new NotificationSource<RouteLocationNormalized>;
    private router: Router;

    /*
     * Adds route change listener
     */
    public addRouteChangeListener(callback: (route: RouteLocationNormalized) => void): void {
        if(!this.router) {
            this.router = useRouter();

            this.router.afterEach((to) => {
                this.routeChangeSource.updateListeners(to);
            });
        }

        this.routeChangeSource.addListener(callback);
    }

    /*
     * Gets current route
     */
    public getRoutes(): Array<RouteRecordRaw> {
        return [
            {
                name: 'Organizations',
                path: '/organizations',
                component: () => import('../../../modules/organizations/components/OrganizationsTab.vue')
            },
            {
                name: 'Products',
                path: '/products',
                component: () => import('../../../modules/products/components/ProductsTab.vue')
            }
            // {
            //     name: 'Prompts',
            //     path: '/prompts',
            // },
            // {
            //     name: 'Workflows',
            //     path: '/workflows',
            // }
        ]
    }
}