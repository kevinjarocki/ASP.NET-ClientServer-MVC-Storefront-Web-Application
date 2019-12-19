// modal component using axios
Vue.component("branchmodal", {
    template: "#modal-template",
    props: {
        details: [],
        lat: '',
        lng: '',
    },
    methods: {
        async showModal() {
            try {
                this.modalStatus = "Getting Map Details..";
                let response = await axios.get(`/GetBranches/${this.lat}/${this.lng}`);
                this.details = response.data; // array processed in for loop below
                this.showModal = true;
                let myLatLng = new google.maps.LatLng(this.lat, this.lng);
                let map_canvas = document.getElementById("map");
                let options = {
                    zoom: 10, center: myLatLng, mapTypeId:
                        google.maps.MapTypeId.ROADMAP
                };
                let map = new google.maps.Map(map_canvas, options);
                let center = map.getCenter();
                let i2 = 0;
                let infowindow = null;
                infowindow = new google.maps.InfoWindow({ content: "holding..." });
                // add the markers, event handlers, infowindows for each location
                this.details.map((detail) => {
                    i2 = i2 + 1;
                    marker = new google.maps.Marker({
                        position: new google.maps.LatLng(detail.latitude, detail.longitude),
                        map: map,
                        animation: google.maps.Animation.DROP,
                        icon: `../images/marker${i2}.png`,
                        title: `Branch#${detail.id} ${detail.street}, ${detail.city}, ${detail.region}`,
                        html: `<div>Branch#${detail.id}<br/>${detail.street}, ${detail.city}<br/> 
                        ${detail.distance.toFixed(2)} mi</div>`
                    });
                    google.maps.event.addListener(marker, "click", function () {
                        infowindow.setContent(this.html); // added .html to the marker object.
                        infowindow.close();
                        infowindow.open(map, this);
                    });
                });
                map.setCenter(center);
                google.maps.event.trigger(map, "resize");
            } catch (error) {
                console.log(error.statusText);
            }
        }
    },
    mounted() {
        this.showModal();
    },
})
// Vue instance using google maps geocoder
var app = new Vue({
    el: "#branches",
    methods: {
        loadAndShowModal() {
            let geocoder = new google.maps.Geocoder(); // A service for converting between anaddress to LatLng
            geocoder.geocode({ "address": this.address }, (results, status) => {
                if (status == google.maps.GeocoderStatus.OK) { // only if google gives us the OK
                    this.lat = results[0].geometry.location.lat();
                    this.lng = results[0].geometry.location.lng();
                    this.showModal = true;
                }
            })
        },
    },
    data: {
        address: "N5Y-5R6",
        lat: "",
        lng: "",
        showModal: false,
    }
});