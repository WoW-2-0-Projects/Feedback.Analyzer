import {defineStore} from "pinia";
import {Account} from "@/modules/accounts/models/Account";

export const useAccountStore = defineStore('account' , {
   state:() => ({
       account:  new Account()
   }),
    actions: {
       set(newAccount: Account) {
           this.account = newAccount;
       },
       remove() {
           this.account = new Account();
       }
    }
} )