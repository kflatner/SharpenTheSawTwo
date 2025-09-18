<script setup>
import { ref, onMounted } from 'vue';
import { http } from '../api/http';

const users = ref([]);
const loading = ref(true);
const error = ref('');

const points = ref ([]); 

onMounted(async () => {
  try {
    const res = await http.get('/getUsers');
    users.value = res.data;
  } catch (e) {
    error.value = 'Failed to load users';
    console.error(e);
  } finally {
    loading.value = false;
  }
});

// testApiMetod (async () => {
//     const res = await http.get('/getUsersWeedstones/2');
//     points.value = res.data;
// });
</script>

<template>
  <main class="page">
    <h1>Users</h1>

    <p v-if="loading">Loading…</p>
    <p v-else-if="error" class="err">{{ error }}</p>

    <ul v-else class="list">
      <li v-for="u in users" :key="u.id" class="item">
        <b>{{ u.userName ?? u.name }}</b>
        <span class="muted">{{ u.email }}</span>
      </li>
    </ul>

    <!-- UserPoints: </br> {{ points.name }}  -->
  </main>
</template>

<style scoped>
.page { max-width: 680px; margin: 32px auto; padding: 0 16px; }
h1 { margin-bottom: 16px; font-size: 24px; }
.list { list-style: none; padding: 0; margin: 0; display: grid; gap: 8px; }
.item { padding: 10px 12px; border: 1px solid #e5e7eb; border-radius: 8px; display: flex; justify-content: space-between; }
.muted { color: #6b7280; }
.err { color: #b91c1c; }
</style>