<template>
        <form @submit.prevent="Login" class="form"> 
            <label>Username:
                <input required v-model="formInputs.name">
            </label>
            
            <br>
            <label>Password:
                <input type="password" required v-model="formInputs.password"></input>
            </label>
            <br>
            <div class="submit">
            <button>Log inn</button>
            <button>register</button>
            </div>
        </form>
        <button v-on:click="giveTest"></button>
</template>

<script>
import { http } from '../api/http';
import { mapState } from 'vuex'
import { useStore } from 'vuex'

const store = useStore()


export default {
    data() {
          return {
            formInputs: {
                id: 0,
                name: '',
                email: '',
                password: '',
                role: '',
                lifepoints: 0,
                weedstones: 0
            },
          };
      },
      computed: {
        ...mapState(['loggedInn'])
      },
      created () {
      },
      methods: {

        async postApiData(url, info, key, type) {
          const res = await http.post(url, info);
        let data = res.data;
        this.$store.commit('updateLoggedInnField', { key: key, value: data[type] })
        },


        async getApiData(url, info, key, type) {
          const res = await http.get(url, info);
        let data = res.data;
        this.$store.commit('updateLoggedInnField', { key: key, value: data[type] })
        },
        async Login() {
        await this.postApiData('/login', this.formInputs, 'id', "userId")
        this.getApiData('/getUsersLifePoints/' + this.loggedInn.id, this.loggedInn.id, 'lifePoints', "lifepoints")
        await this.getApiData('/getUsersWeedstones/'+ this.loggedInn.id, this.loggedInn.id, 'weedstones', "weedstones")

    return false
        },  
        giveTest() {
          this.$store.commit('updateLoggedInnField', { key: 'lifepoints', value: 50 })
          const res = http.post('/setUsersLifePoints/1', 1, this.formInputs);
        }
        }
      }
</script>
