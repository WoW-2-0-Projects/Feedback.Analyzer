import {createRouter, createWebHistory} from 'vue-router'
import {RouterService} from "@/infrastructure/services/router/RouterService";

const router = createRouter({
    history: createWebHistory(import.meta.env.BASE_URL),
    routes: new RouterService().getRoutes(),
});

export default router;