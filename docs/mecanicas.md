# Mecánicas

Las principales mecánicas en las que se centra el juego son, su género bullet hell, y la  
mecánica tipo [osu](https://osu.ppy.sh/) para castear poderes/hechizos realizando un patrón  
rápidamente al ritmo de la música.

## Bullet Hell

Un bullet hell es un subgénero de los juegos de disparos en los que hay que disparar y  
esquivar un gran número de balas. Tanto que, a veces, la pantalla queda colapsada por  
los proyectiles y es difícil de ver.

## Jugador

### Movimiento

El jugador puede moverse en direcciones cardinales. Además, se puede esquivar hacia una de estas direcciones volviéndose inmune a todo tipo de daño.

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

## Entorno

El entorno consta de 3 pisos, aumentando de dificultad según se progrese. Cada uno es un conjunto  
de salas preconstruidas. Cuando el jugador entra a una de estas por primera vez, aparecerán varios  
enemigos y, a la vez, se cerrarán las salidas. Una vez todos los enemigos sean derrotados, se abrirá el  
camino a la siguiente sala.

Al completar la última sala del piso, el jugador recuperará toda la vida perdida.

### Cámara

La cámara está centrada en el jugador y esta lo sigue mientras se mueve.

## Enemigos

Criaturas hostiles que buscan capturar al jugador para evitar su escape de la mazmorra.

### Regulares

Los enemigos persiguen y disparan al jugador. Los disparos varían en tamaño y patrón de lanzamiento.  
Los enemigos de pisos superiores pueden lanzar patrones más complicados.

### Jefes

Enemigos finales de cada piso. Cuentan con una gran cantidad de vida y son capaces de disparar muchas  
balas por sí solos.