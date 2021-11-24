from django.shortcuts import get_object_or_404, redirect, render
from django.contrib.auth.forms import AuthenticationForm
from .models import Vehicle, Tire, Sensor

# Create your views here.
def indexPage(request):
    form = AuthenticationForm()

    if request.method == "POST":
        return redirect('home')

    context = {'form':form}
    return render(request,'index/index.html', context)

def homePage(request):
    vehicles = Vehicle.objects.all()

    context = {'vehicles':vehicles}
    return render(request, 'user/home.html', context)

def vehiclePage(request, pk):
    vehicle = get_object_or_404(Vehicle, id=pk)
    tires = vehicle.tires.all()

    context = {'vehicle':vehicle, 'tires':tires}
    return render(request, 'user/vehicle.html', context)

def tirePage(request, pk):
    tire = get_object_or_404(Tire, id=pk)

    context = {'tire':tire}
    return render(request, 'user/tire.html', context)

def sensorPage(request, pk):
    sensor = get_object_or_404(Sensor, id=pk)

    context = {'sensor':sensor}
    return render(request, 'user/sensor.html', context)

# http://127.0.0.1:8000/savedata/AUTHSYSTEM-TEST111-SENSORTEST1-3234-223-BROKEN-SENSORTEST2-40234-222-WORKING-SENSORTEST3-2237-32345-DANGER-SENSORTEST4-5453-1354-WARNING
def saveData(request, query):
    chunks = query.split('-')
    if chunks[0] == 'AUTHSYSTEM':
        vehicle = get_object_or_404(Vehicle,id=chunks[1])
        tires = vehicle.tires.all()
        counter = 0
        for x in range(2, len(chunks), 4):
            sensor = tires[counter].sensor
            sensor.temperature = chunks[x+1]
            sensor.pressure = chunks[x+2]
            sensor.status = chunks[x+3]
            sensor.save()
            counter+=1
            
    return redirect('/')
