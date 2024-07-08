<?php
include_once '../includes/cdn.php'; ?>
<!DOCTYPE html>
<html lang="en">

<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Admin Dashboard</title>
</head>

<body>

    <div class="wrapper">
        <?php include '../Admin/includes/sidebar.php'; ?>
        <div class="main">
            <nav class="navbar custom-toggler navbar-expand-lg px-3 border-bottom">
                <button class="btn" id="sidebar-toggle" type="button">
                    <span class="navbar-toggler-icon "></span>
                </button>
                <div class="navbar-collapse navbar p-0 d-flex justify-content-end align-items-center">
                    <span>Welcome back <b>Lemuel</b>!</span>
                    <a href="#" class="las la-user-circle ps-2"></a>
                </div>
            </nav>

            <main class="content px-3 py-2 ">

                <div class="container-fluid border border-primary">
                    <div class="row">
                        <div class="col-md-6 border border-dark">
                            <canvas id="myChart"></canvas>
                        </div>

                        <div class=" col-md-6 border border-dark">
                            <canvas id="myLineChart"></canvas>
                        </div>
                    </div>


                    <div class="row">
                        <div class="col-md-6 border border-dark">
                            <canvas id="myRadarChart"></canvas>
                        </div>
                        <div class="col-md-6 border border-dark">
                            <canvas id="myRadarChart"></canvas>
                        </div>
                    </div>

                </div>

            </main>



        </div>
    </div>
    <script src="../Scripts/barchart.js"></script>
    <script src="../Scripts/linegraph.js"></script>
    <script src="../Scripts/radarchart.js"></script>


</body>

</html>