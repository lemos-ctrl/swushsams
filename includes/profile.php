<?php
include_once '../includes/cdn.php'; ?>

<!DOCTYPE html>
<html lang="en">

<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Attendance Page</title>
</head>



<body>
    <div class="wrapper">
        <div class="main">

            <main class="content px-3 py-2 ">


                <div class="row gutters-sm">
                    <div class="col-md-4 mb-3">
                        <div class="card">
                            <div class="card-body">
                                <div class="d-flex flex-column align-items-center text-center">
                                    <img src="https://bootdey.com/img/Content/avatar/avatar7.png" alt="Admin" class="rounded-circle" width="150">
                                    <div class="mt-3">
                                        <h4>Tiger Nixon</h4>
                                        <p class="text-secondary mb-1">Teacher</p>
                                        <p class="text-muted font-size-sm">Urgello, Cebu City, Philippines</p>
                                        <!--<button class="btn btn-primary">Follow</button>
                                        <button class="btn btn-outline-primary">Message</button>-->
                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>
                    <div class="col-md-8">
                        <div class="card mb-3">
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-sm-3">
                                        <h6 class="mb-0">Full Name</h6>
                                    </div>
                                    <div class="col-sm-9 text-secondary">
                                        John Doe
                                    </div>
                                </div>
                                <hr>
                                <div class="row">
                                    <div class="col-sm-3">
                                        <h6 class="mb-0">Email</h6>
                                    </div>
                                    <div class="col-sm-9 text-secondary">
                                        johndoe@email.com
                                    </div>
                                </div>
                                <hr>
                                <div class="row">
                                    <div class="col-sm-3">
                                        <h6 class="mb-0">Phone</h6>
                                    </div>
                                    <div class="col-sm-9 text-secondary">
                                        +69300000000
                                    </div>
                                </div>
                                <hr>
                                <div class="row">
                                    <div class="col-sm-3">
                                        <h6 class="mb-0">Mobile</h6>
                                    </div>
                                    <div class="col-sm-9 text-secondary">
                                        +69300000000
                                    </div>
                                </div>
                                <hr>
                                <div class="row">
                                    <div class="col-sm-3">
                                        <h6 class="mb-0">Address</h6>
                                    </div>
                                    <div class="col-sm-9 text-secondary">
                                        Urgello, Cebu City, Philippines
                                    </div>
                                </div>
                                <hr>
                                <div class="row">
                                    <div class="col-sm-12">
                                        <a class="btn btn-info " target="__blank">Edit</a>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="card">
                        <div id='calendar' class="mt-2"></div>
                    </div>

                    <script src='https://cdn.jsdelivr.net/npm/fullcalendar/index.global.min.js'></script>
                    <script>
                        document.addEventListener('DOMContentLoaded', function() {
                            const calendarEl = document.getElementById('calendar')
                            const calendar = new FullCalendar.Calendar(calendarEl, {
                                initialView: 'dayGridMonth'
                            })
                            calendar.render()
                        })
                    </script>


                </div>

            </main>


        </div><!-- main -->
    </div><!-- wrapper -->


</body>



</html>