import vue from 'vue'
import router from 'vue-router'
import Index from "@/views/Index"
vue.use(router)

export default new router({
    mode:"history",
    routes: [
        {
            name:"Index",
            path:"/",
            component: Index

        }
    ]
})