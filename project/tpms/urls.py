from django.urls import path
from .views import *

urlpatterns = [
    path('', indexPage, name='index'),
    path('home', homePage, name='home'),
    path('admin-page', adminPage, name='admin-page'),
    path('add-fleet-manager', addFleetManager, name='add-fleet-manager'),

    path('logout', logoutPage, name='logout'),

    path('vehicle/<str:pk>', vehiclePage, name='vehicle'),
    path('add-vehicle/', addVehiclePage, name='add-vehicle'),

    path('tire/<str:pk>', tirePage, name='tire'),
    # path('add-tire/', addTirePage, name='add-tire'),

    path('sensor/<str:pk>', sensorPage, name='sensor'),
    # path('add-sensor/', addSensorePage, name='add-sensor'),

    path('savedata/<str:query>', saveData, name='savedata'),
]