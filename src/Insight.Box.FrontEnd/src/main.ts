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

const app = createApp(App);

// Set state management
app.use(createPinia());

// Set router
app.use(router);

app.mount('#app');