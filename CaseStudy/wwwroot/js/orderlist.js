// register modal component
Vue.component("modal", {
    template: "#modal-template",
    props: {
        order: {},
        details: []
    },
})
// main Vue component
new Vue({
    el: "#orders",
    methods: {
        async getOrders() {
            try {
                this.status = "Loading Orders..."
                response = await axios.get("/GetOrders");
                this.orders = response.data;
                this.status = ""
            } catch (error) {
                console.log(error.statusText);
            }
        },
        async loadAndShowModal() {
            try {
                this.modalStatus = "Loading Details..";
                this.showModal = true;
                response = await axios.get(`/GetOrderDetails/${this.orderForModal.id}`);
                this.detailsForModal = response.data;
                this.modalStatus = "";
            } catch (error) {
                console.log(error.statusText);
            }
        },
    },
    mounted() { this.getOrders() },
    data: {
        orders: [],
        showModal: false,
        orderForModal: {},
        detailsForModal: [],
        status: "",
        modalStatus: ""
    }

});


function formatDate(date) {
    let d;
    // see if date is coming from server
    date === undefined
        ? d = new Date()
        : d = new Date(Date.parse(date)); // date from server
    let _day = d.getDate();
    let _month = d.getMonth() + 1;
    let _year = d.getFullYear();
    let _hour = d.getHours();
    let _min = d.getMinutes();
    if (_min < 10) { _min = "0" + _min; }
    return _year + "-" + _month + "-" + _day + " " + _hour + ":" + _min;
}
Vue.filter('toCurrency', function (value) {
    if (typeof value !== "number") {
        return value;
    }
    var formatter = new Intl.NumberFormat('en-US', {
        style: 'currency',
        currency: 'USD',
        minimumFractionDigits: 0
    });
    return formatter.format(value);
});