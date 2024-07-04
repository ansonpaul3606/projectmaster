var app = new Vue({
    el: '#app',
    mixins: [ VueFocus.mixin ],
    data: {
        searchOpen: false
    },
    methods: {
        toggleOpen: function() {
            this.searchOpen = true;
            anime({
                targets: '.search',
                duration: 1000,
                elasticity: 400,
                width: 250
            });
            anime({
                targets: '.search-btn',
                duration: 1000,
                backgroundColor: '#FFF',
                color: '#FF3B42',
                elasticity: 200,
                translateX: 6
            });
        },
        toggleClose: function() {
            this.searchOpen = false;
            anime({
                targets: '.search',
                duration: 700,
                elasticity: 200,
                width: 40
            });
            anime({
                targets: '.search-btn',
                duration: 700,
                backgroundColor: '#FF3B42',
                color: '#fff',
                elasticity: 200,
                translateX: 0
            });
        }
    }
})