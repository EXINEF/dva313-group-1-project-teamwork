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
    
    path('vehicle/<str:pk>', vehicle, name='vehicle'),
    path('add-vehicle/', addVehicle, name='add-vehicle'),
    path('edit-vehicle/<str:pk>', editVehicle, name='edit-vehicle'),
    path('delete-vehicle/<str:pk>', deleteVehicle, name='delete-vehicle'),

    path('tire/<str:pk>', tire, name='tire'),
    path('add-tire/', addTire, name='add-tire'),
    path('edit-tire/<str:pk>', editTire, name='edit-tire'),
    path('delete-tire/<str:pk>', deleteTire, name='delete-tire'),

    path('sensor/<str:pk>', sensor, name='sensor'),
    path('add-sensor/', addSensor, name='add-sensor'),
    path('edit-sensor/<str:pk>', editSensor, name='edit-sensor'),
    path('delete-sensor/<str:pk>', deleteSensor, name='delete-sensor'),

    # admin
    path('admin-page', adminPage, name='admin-page'),
    path('add-fleet-manager', addFleetManager, name='add-fleet-manager'),
    path('edit-fleet-manager/<str:pk>', editFleetManager, name='edit-fleet-manager'),
    path('delete-fleet-manager/<str:pk>', deleteFleetManager, name='delete-fleet-manager'),
    
    #others
    path('savedata/<str:query>', saveData, name='savedata'),
]