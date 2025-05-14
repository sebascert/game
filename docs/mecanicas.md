# Mecánicas

Las principales mecánicas en las que se centra el juego son, su género bullet hell, y la  
mecánica tipo [osu](https://osu.ppy.sh/) para castear poderes/hechizos realizando un patrón  
rápidamente al ritmo de la música.

## Bullet Hell

Un bullet hell es un subgénero de los juegos de disparos en los que hay que disparar y  
esquivar un gran número de balas. Tanto que, a veces, la pantalla queda colapsada por  
los proyectiles y es difícil de ver.

## Jugador

### Vitalidad

El jugador cuenta con un contador de "vida". Si este se reduce a 0 se pierde el juego.
El contador tiene un valor maximo de 5, este reduce al recibir daño y se recupera tras 
completar un piso.

### Movimiento

El jugador puede moverse en direcciones cardinales (norte, sur, este, oeste) y diagonales. 
Además, se puede "esquivar" hacia una de estas direcciones volviéndose inmune a todo tipo 
de daño. No debe de ser utilizable todo el tiempo, es decir debe de tener un tiempo de espera 
hasta poder usarse otra vez. Tambien al final de este hay una pequeña fraccion de tiempo en 
la que el jugador no puede moverse a ninguna direccion pero si es capaz de tomar daño

### PowerCast

Manera principal en la que el jugador puede atacar. Disponible solamente durante un combate.

#### ¿Cómo funciona?  
Durante el combate aparecerán "notas" musicales alrededor de la pantalla.  
Estas deberán de ser presionadas a tiempo, preferiblemente sin fallar ninguna. Al completar el patrón,  
un proyectil / onda de daño saldrá disparada al enemigo más cercano.

También se hace uso de un contador de "combo". Por cada nota tocada sin fallar este contador aumentará.  
Mientras más alto sea, los ataques del personaje harán más daño y, mientras más alto sea el combo,  
los patrones siguientes se volverán más complicados. El contador regresa a 0 al fallar, anulando o reduciendo  
el daño del ataque.

#### Definicion de dificultad

Un patron de notas mas dificil consta de una combinación de mayor cantidad de notas, menor timepo de ejecucion 
de cada nota y menor tiempo de ejecucion del patron entero.
La posicion de las notas son generadas es aleatoriamente. El espacio en el que pueden aparecer es limitado a
un espacio menor que la vista del jugador evitando tapar elemtos de la ui.

#### Forma y vizuales

Al completar un patron se lanzara un ataque. Se vera como una onda expansiva en forma de circulo de notas 
diferentes a las del powercast tendra un rango limitado y ignorara obstaculos en el entorno. Este golpea 
a todos los enemigos que alcanze. No causa retroseso.

#### Daño segun el combo

Como ya se menciono mientras mas alto el combo mayor sera el daño. La manera en la que se calculara el daño
sera la siguiente: $Daño = dañoBase + combo\times2$. Dañp base es un valor arbitrario por decidir. 
Esta formula solo aplica al primer enemigo golpeado, por cada enemigo que sea alcanzado por el ataque 
el daño que recibiran se vera reducido a la mitad.

## Entorno

El entorno consta de 3 pisos, aumentando de dificultad según se progrese. Cada uno es un conjunto  
de salas preconstruidas. Cuando el jugador entra a una de estas por primera vez, aparecerán varios  
enemigos y, a la vez, se cerrarán las salidas. Una vez todos los enemigos sean derrotados, se abrirá el  
camino a la siguiente sala. El layout de la sala no cmabia y hay varios caminos, solamente 1 lleva a la
sala del jefe mientras que los demas eventualmente ya no conectan a nuevas salas. Es importante 
que el jugador pueda regresar a salas anteriores en caso de escojer el camino correcto.

Al completar la última sala del piso, el jugador recuperará toda la vida perdida.

### Cámara

La cámara está centrada en el jugador y esta lo sigue mientras se mueve.

## Enemigos

Criaturas hostiles que buscan capturar al jugador para evitar su escape de la mazmorra.
Los enemigos persiguen y disparan al jugador, estos aparecen al entrar a una sala y desaparecen 
al ser derrotados. No se regeneran. Los disparos varían en tamaño y patrón de lanzamiento.  
Los enemigos de pisos superiores pueden lanzar patrones de balas más complicados.

El comportaminto de la ia en enemigos regulares y de elite es la misma. Mantendran su distancia 
al jugador pero mantendran una linea de vision. Cuando tengan linea de vision al jugador haran 
sus ataques

Solamente para enemigos regulares existen los enemigos cuerpo a cuerpo los cuales tienen un 
comportamiento diferente de ia. Estos perseguiran al jugador y al alcanzarlo haran un ataque cuerpo
a cuerpo.

### Regulares
- vida regular
- Tipos de ataque: cuerpo a cuerpo, bullets
- tamaño: igual o menor al jugador

### Elites  
Los elites cuentan con un escudo. Mientras este este activado haran el doble de daño. El escudo
no puede tener un valor mayor a un tercio de la vida original y no se puede regenerar

- vida buena
- escudo regular: ninguna
- regeneracion de escudo
- tamaño: mayor al jugador
- tipos de ataques: unicamente bullets

### Jefes

Enemigos finales de cada piso. Cuentan con una gran cantidad de vida y son capaces de disparar muchas  
balas por sí solos. Al igual que los elites cuentan con un escudo que no puede superar la mitad de la vida original.
Su comportamiento de ia es muy diferente a los demas. Se quedan estaticos al centro de la sala disparando sus patrones
de balas regularmente.

- vida excelente
- escudo bueno
- regeneracion: al golpear al jugador recuperan 33%
- tamaño: mucho mas gande que el jugador
- tipos de ataques: unicamente bullets