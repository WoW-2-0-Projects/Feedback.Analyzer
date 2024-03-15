import {defineStore} from "pinia";
import {Account} from "@/modules/accounts/models/Account";

export const useAccountStore = defineStore('account' , {
   state:() => ({
       account:  new Account()
   }),
    actions: {
       set(newAccount: Account) {
           this.account = newAccount;
           console.log('set account ', this.account);
       },
       remove() {
           this.account = new Account();
       }
    }
} )