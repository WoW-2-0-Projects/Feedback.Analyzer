import './assets/main.css'
import './assets/formStyles.css';
import './assets/modalStyles.css';
import './assets/cardStyles.css';
import './assets/tableStyles.css';
import './assets/chartStyles.css';

import { createApp } from 'vue'
import { createPinia } from 'pinia'

import App from './App.vue'
import router from "@/infrastructure/router/Router";
import {AuthenticationService} from "@/modules/accounts/services/AuthenticationService";

const app = createApp(App);

// Set state management
app.use(createPinia());

// Set router
app.use(router);

// Set account service
const authService = new AuthenticationService();
await authService.setCurrentUser();

app.mount('#app');