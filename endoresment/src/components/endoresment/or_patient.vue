<template>
    <div class="or_patient">

    </div>
</template>

<script>
export default {
    name:'or_patient',
    props: ['link'],
    data() {
        return {
            apiUrl: `http://localhost:${this.link}/endoresment/dist/`,
        }
    }
}
</script>

<style scoped>

</style>