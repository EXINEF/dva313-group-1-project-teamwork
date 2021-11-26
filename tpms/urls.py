from django.urls import path
from .views import homePage, indexPage, vehiclePage, tirePage, sensorPage, saveData

urlpatterns = [
    path('', indexPage, name='index'),
    path('home', homePage, name='home'),
    path('vehicle/<str:pk>', vehiclePage, name='vehicle'),
    path('tire/<str:pk>', tirePage, name='tire'),
    path('sensor/<str:pk>', sensorPage, name='sensor'),
    path('savedata/<str:query>', saveData, name='savedata'),
]