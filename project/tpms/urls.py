from django.urls import path
from .views import *
from .views_admin import *

urlpatterns = [
    # authentication
    path('', indexPage, name='index'),
    path('logout', logoutPage, name='logout'),

    # fleet manager
    path('home', homePage, name='home'),
    path('home-extended', homePageExtended, name='home-extended'),
    
    path('vehicle/<str:pk>', vehiclePage, name='vehicle'),
    path('add-vehicle/', addVehiclePage, name='add-vehicle'),
    path('update-vehicle/<str:pk>', updateVehiclePage, name='update-vehicle'),
    path('delete-vehicle/<str:pk>', deleteVehiclePage, name='delete-vehicle'),

    path('tire/<str:pk>', tirePage, name='tire'),
    path('add-tire/', addTirePage, name='add-tire'),

    path('sensor/<str:pk>', sensorPage, name='sensor'),
    path('add-sensor/', addSensorPage, name='add-sensor'),

    # admin
    path('admin-page', adminPage, name='admin-page'),
    path('add-fleet-manager', addFleetManager, name='add-fleet-manager'),
    path('edit-fleet-manager/<str:pk>', editFleetManager, name='edit-fleet-manager'),
    path('delete-fleet-manager/<str:pk>', deleteFleetManager, name='delete-fleet-manager'),
    
    #others
    path('savedata/<str:query>', saveData, name='savedata'),
]